# Pdf Api

Html to Pdf rendering API, implemented with PuppeteerSharp and Asp.Net Core. Built and published as `jmedinap/pdfapi:latest` image at [docker hub](https://hub.docker.com/repository/docker/jmedinap/pdfapi/general)

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

```bash
curl -XPOST --form-string "html=<body>test multipart</body>" --form-string "headerTemplate=<div style='font-size:12px'>test header</div>" --form-string "footerTemplate=<div style='font-size:12px'>page <span class='pageNumber'></span></div>" --form-string "marginTop=200px" --form-string "marginBottom=100px" --form-string "marginLeft=50px" --form-string "marginRight=50px" -H'content-type: multipart/form-data' -H'x-api-key: XXXXXX' -o test.pdf http://localhost:5005/pdfapi/fromhtml
```
