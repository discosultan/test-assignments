import delay from './util/delay'

async function welcome() {
    console.log('Hello');

    for (let i = 0; i < 3; i++) {
        await delay(500);
        console.log(".");
    }

    console.log('World!');

    // (function loop(i) {
    //     if (i < 3) {
    //         console.log('.');
    //         delay(500).then(() => loop(i + 1));
    //     } else {
    //         console.log('World!');
    //     }
    // })(0);
}

welcome();