import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { GameResultShort } from "../models/game";
import {format} from 'date-fns';
import { store } from "./store";
import { Pagination, PagingParams } from "../models/pagination";
import { User } from "../models/user";

export default class HistoryStore {
    gameRegistry = new Map<string, GameResultShort>();
    user: User | null = null
    selectedGame: GameResultShort | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();
    predicate = new Map().set('all', true);

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.predicate.keys(),
            () => {
                this.pagingParams = new PagingParams();
                this.gameRegistry.clear();
                this.loadHistory();
            }
        )
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    setPredicate = (predicate: string, value: string | Date) => {
        const resetPredicate = () => {
            this.predicate.forEach((value, key) => {
                if (key !== 'startDate') this.predicate.delete(key);
            })
        }
        switch (predicate) {
            case 'all':
                resetPredicate();
                this.predicate.set('all', true);
                break;
            case 'isGoing':
                resetPredicate();
                this.predicate.set('isGoing', true);
                break;
            case 'isHost':
                resetPredicate();
                this.predicate.set('isHost', true);
                break;
            case 'startDate':
                this.predicate.delete('startDate');
                this.predicate.set('startDate', value);
        }
    } 

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('pageNumber', this.pagingParams.pageNumber.toString());
        params.append('pageSize', this.pagingParams.pageSize.toString());
        this.predicate.forEach((value, key) => {
            if (key === 'startDate') {
                params.append(key, (value as Date).toISOString())
            } else {
                params.append(key, value);
            }
        })
        return params;
    }

    get activitiesByDate() {
        return Array.from(this.gameRegistry.values()).sort((a, b) =>
            a.startTime!.getTime() - b.startTime!.getTime());
    }

    get groupedHistory() {
        return Object.entries(
            this.activitiesByDate.reduce((activities, game) => {
                const date = format(game.startTime!, 'dd MMM yyyy');
                activities[date] = activities[date] ? [...activities[date], game] : [game];
                return activities;
            }, {} as {[key: string]: GameResultShort[]})
        )
    }

    loadHistory = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.History.list(this.axiosParams);
            result.data.forEach(game => {
                this.setGame(game);
            })
            this.setPagination(result.pagination);
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    loadGame = async (id: string) => {
        let game = this.getGame(id);
        if (game) {
            this.selectedGame = game;
            return game;
        } else {
            this.loadingInitial = true;
            try {
                game = await agent.History.details(id);
                this.setGame(game);
                runInAction(() => {
                    this.selectedGame = game;
                })
                this.setLoadingInitial(false);
                return game;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }

    private setGame = (game: GameResultShort) => {
        const user = store.userStore.user;
        this.user = user;
        game.startTime = new Date(game.startTime!);
        this.gameRegistry.set(game.id, game);
    }

    private getGame = (id: string) => {
        return this.gameRegistry.get(id);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }
    
    clearSelectedGame = () => {
        this.selectedGame = undefined;
    }
}