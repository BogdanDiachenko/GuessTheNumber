export interface User{
    id: string;
    userName: string;
    email: string;
    token: string;
}

export interface LoginViewModel{
    email: string;
    password: string;
}

export interface RegisterViewModel {
    email: string;
    password: string;
    confirmPassword: string;
    name: string;
    userName: string;
    surname: string;
}