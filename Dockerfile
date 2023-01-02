FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/core/aspnet:3.1

LABEL mantainer="Johann M <jmedinap@gmail.com>"
ENV PORT=5000

RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list \ 
    && apt-get update \
    && apt-get -y install fonts-symbola fonts-ocr-b fonts-freefont-ttf ttf-mscorefonts-installer \
    && apt-get -y install xorg openbox libnss3 libasound2 \
    && apt-get -y install curl \
    && rm -rf /var/lib/apt/lists/* 

COPY bin/Release/netcoreapp3.1/publish /pdfapi

WORKDIR /pdfapi
RUN dotnet PdfApi.dll --initBrowser

ENTRYPOINT dotnet PdfApi.dll --urls "http://0.0.0.0:$PORT"
HEALTHCHECK CMD curl --fail -s "http://localhost:$PORT/health" || exit 