# Pdf Api

Html to Pdf rendering API, implemented with PuppeteerSharp and Asp.Net Core

## Run

```bash
docker run -d --rm --name pdfapi -p 5005:5005 -e PORT=5005 -e API_KEY=XXXXXX jmedinap/pdfapi:latest
```

## Test

```bash
curl -XPOST -d'<body>test</body>' -H'content-type: text/html' -H'x-api-key: XXXXXX' -o test.pdf http://localhost:5005/pdfapi/fromhtml
```

```bash
curl -XPOST -d'<body>test encrypted</body>' -H'content-type: text/html' -H'x-api-key: XXXXXX' -H'x-encrypt-pass: ZZZZZZ' -o test.pdf http://localhost:5005/pdfapi/fromhtml
```
