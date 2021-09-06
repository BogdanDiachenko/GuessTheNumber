import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import UserStore from "./userStore";
import GameStore from './gameStore'
import HistoryStore from "./historyStore";

interface Store {
    historyStore: HistoryStore;
    commonStore: CommonStore;
    userStore: UserStore;
    modalStore: ModalStore;
    gameStore: GameStore;
}

export const store: Store = {
    historyStore: new HistoryStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore(),
    gameStore: new GameStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}