import { Role } from "./role";

export interface User {
    firstName: string,
    lastName: string,
    email: string,
    roles: [Role],
    token?: string,
}
