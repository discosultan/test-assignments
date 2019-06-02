import * as fs from 'fs';
import * as os from 'os';
import * as path from 'path';
import { promisify } from 'util';

export async function moveToHome(srcFile: string, dstFile: string): Promise<void> {
    await fs.promises.rename(srcFile, await fullPath(dstFile));
}

export async function readFile(file: string): Promise<Buffer> {
    return await fs.promises.readFile(await fullPath(file));
}

async function fullPath(file: string): Promise<string> {
    const dir = path.join(os.homedir(), '.embark', 'user');
    if (!await promisify(fs.exists)(dir)) await fs.promises.mkdir(dir, { recursive: true });
    return path.join(dir, file);
}
