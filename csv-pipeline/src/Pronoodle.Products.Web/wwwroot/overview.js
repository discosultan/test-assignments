(() => {
    streamProducts();

    const table = document.querySelector("#overview-table-body");
    const filterProps = ["key", "discountPrice"];
    const maxProductRowCount = 2000;

    // Would highly benefit from a more declarative programming style. Especially when mutating DOM.
    function streamProducts() {
        const ws = new WebSocket(`ws://${window.location.host}/overview`);
        let productRowCount = 0;
        const productRows = {};

        ws.onmessage = processServerMsg;

        function processServerMsg(msg) {
            const products = JSON.parse(msg.data).data;
            for (let product of products) {
                // Existing product rows will be replaced by new ones.
                const existingRow = productRows[product.key];
                if (existingRow) {
                    table.removeChild(existingRow);
                } else {
                    productRowCount++;
                }

                addProduct(product);
            }

            function addProduct(product) {
                const row = document.createElement("tr");
                for (let prop of Object.keys(product).filter(key => !filterProps.includes(key))) {
                    const td = document.createElement("td");
                    switch (prop) {
                        case "price": appendPriceData(td, product[prop]); break;
                        default: appendDefault(td, product[prop]); break;
                    }
                    row.appendChild(td);
                }

                table.insertBefore(row, table.firstElementChild);

                if (productRowCount > maxProductRowCount) {
                    productRowCount = maxProductRowCount;
                    table.removeChild(table.lastElementChild);
                }

                productRows[product.key] = row;

                function appendPriceData(td, value) {
                    if (product.discountPrice) {
                        const s = document.createElement("s");
                        s.appendChild(document.createTextNode(value));
                        td.appendChild(s);
                        td.appendChild(document.createTextNode(` ${product.discountPrice} (discount)`));
                    } else {
                        appendDefault(td, value);
                    }
                }

                function appendDefault(td, value) {
                    td.appendChild(document.createTextNode(value));
                }
            }
        }
    }
})();
