(() => {
    // TODO: Progress bar(s).

    // Global export.
    window.onUpload = evt => {
        const files = evt.target.files;

        // NB! File reader API does not implement an iterator in Edge.
        //for (let file of evt.target.files) uploadFile(file);
        for (let i = 0; i < files.length; i++) uploadFile(files[i]);
    };

    const upConsole = document.querySelector("#upload-console");
    const maxConsoleEntryCount = 250;
    const decoderOptions = { stream: true };
    const newline = "\r\n";
    const chunkSize = 1024;

    // Would highly benefit from a more declarative programming style. Especially when mutating DOM.
    function uploadFile(file) {
        const ws = new WebSocket(`ws://${window.location.host}/upload`);
        let consoleEntryCount = 0;
        let bytesRead = 0;
        let msgsSent = 0;
        let msgsReceived = 0;

        ws.onopen = streamFileToServer;
        ws.onmessage = processServerMsg;

        function streamFileToServer() {
            const reader = new FileReader();
            // TODO: Polyfilled for Edge. Ideally we would work with byte buffers directly and then
            //       the polyfill could be removed.
            const decoder = new TextDecoder();
            let csvBuilder = "";

            reader.onload = processReadChunk;
            reader.onerror = err => outputToConsole([err]);

            read();

            function read() {
                const slice = file.slice(bytesRead, bytesRead + chunkSize);
                reader.readAsArrayBuffer(slice);
            }

            function processReadChunk() {
                bytesRead += chunkSize;

                csvBuilder += decoder.decode(reader.result, decoderOptions);
                const lastLineBreak = csvBuilder.lastIndexOf(newline);

                const csvChunk = csvBuilder.substring(0, lastLineBreak);
                csvBuilder = csvBuilder.substring(lastLineBreak + newline.length);

                ws.send(JSON.stringify({
                    data: csvChunk
                }));
                msgsSent++;

                if (bytesRead < file.size) {
                    read();
                }
            }
        }

        function processServerMsg(msg) {
            msgsReceived++;
            const data = JSON.parse(msg.data).data;

            let text = `[${new Date().toISOString()}] ${data.successes.length} product(s) successfully imported`;
            if (data.errors.length > 0) text += `; ${data.errors.length} failed:`;
            outputToConsole([text, ...data.errors]);

            // Close the socket after having received all the messages.
            if (bytesRead >= file.size && msgsReceived === msgsSent) ws.close();
        }

        function outputToConsole(lines) {
            // Because we insert to top and not bottom, we need to add rows within a text block in
            // reverse order for the output to make sense.
            for (let line of lines.reverse()) {
                const row = document.createElement("div");
                row.appendChild(document.createTextNode(line));
                upConsole.insertBefore(row, upConsole.firstElementChild);

                consoleEntryCount++;

                if (consoleEntryCount > maxConsoleEntryCount) {
                    consoleEntryCount = maxConsoleEntryCount;
                    upConsole.removeChild(upConsole.lastElementChild);
                }
            }
        }
    }
})();
