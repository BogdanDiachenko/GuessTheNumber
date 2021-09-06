import { Id } from "react-toastify";

export interface GameResultShort {
    id : string,
    winner: string | null,
    host: string,
    playersCount: number,
    startTime: Date,
    endTime: Date,
    guessedNumber: number
}
 
export interface GameResultFull{
    
}

export interface GameForm{
    guessedNumber: number
}
export interface GameDto{
    guessedNumber: string,
    hostId: string,
    winnerId: string,
    playersId: string[],
    isFinished: boolean,
    steps: Step[]
}

export interface Step{

}
 