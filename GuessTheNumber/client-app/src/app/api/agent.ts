import axios, { AxiosError, AxiosResponse } from 'axios';
import { request } from 'http';
import { toast } from 'react-toastify';
import { history } from '../..';
import { GameDto, GameForm, GameResultShort} from '../models/game';
import { PaginatedResult } from '../models/pagination';
import { User, LoginViewModel, RegisterViewModel } from '../models/user';
import { store } from '../stores/store';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers.Authorization = `Bearer ${token}`
    return config
})

axios.interceptors.response.use(async response => {
    await sleep(1000);
    const pagination = response.headers['pagination'];
    if (pagination) {
        response.data = new PaginatedResult(response.data, JSON.parse(pagination));
        return response as AxiosResponse<PaginatedResult<any>>
    }
    return response;
}, (error: AxiosError) => {
    const { data, status, config } = error.response!;
    switch (status) {
        case 400:
            if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
                history.push('/not-found');
            }
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key])
                    }
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 404:
            history.push('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            history.push('/server-error');
            break;
    }
    return Promise.reject(error);
})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const History = {
    list: (params: URLSearchParams) => axios.get<PaginatedResult<GameResultShort[]>>('/history', { params })
        .then(responseBody),
    details: (id: string) => requests.get<GameResultShort>(`/history/${id}`),
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: LoginViewModel) => requests.post<User>('/account/login', user),
    register: (user: RegisterViewModel) => requests.post<User>('/account/register', user)
}

const Game = {
    current: () => requests.get<GameDto>('/game/isGameActive'),
    startGame: (game: GameForm) => requests.post<GameDto>('/game/startGame', game),
    // joinGame: (null) => requests.post<GameDto>('/game/joinGame')
    
}

const agent = {
    History,
    Account,
    Game
}

export default agent;