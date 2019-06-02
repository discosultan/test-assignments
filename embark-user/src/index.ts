
import { getConnectionOptions } from 'typeorm';

import App from './app';

(async () => {
    try {
        // TODO: sqlite db file should also be stored to app data folder (~/.embark/user).
        const connectionOptions = await getConnectionOptions();
        await App.run(connectionOptions);
    } catch (e) {
        console.error(e);
    }
})();
