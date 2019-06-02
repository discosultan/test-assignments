import * as bcrypt from 'bcrypt';
import { promisify } from 'util';

export function hashPassword(password: string): Promise<string> {
    // TODO: Consider argon2 as a better alternative for bcrypt.
    const saltRounds = 10;
    return promisify(bcrypt.hash)(password, saltRounds);
}
