import { makeAutoObservable, reaction, runInAction } from 'mobx';
import React from 'react'
import agent from '../api/agent';
import { GameDto, GameForm } from '../models/game'
import { User } from '../models/user';



export default class Game{
    game: GameDto | null = null;
    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    getCurrentGame = async () => {
        try {
            const game = await agent.Account.current();
            runInAction(() => this.user = game);
            console.log('hellooo')
        } catch (error) {
            console.log(error);
        }
    }

    logGame = () => {
        console.log(this.user);
    }

   
}