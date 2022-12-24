FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/core/aspnet:3.1

LABEL mantainer="Johann M <jmedinap@gmail.com>"
ENV PORT=5000

RUN apt-get update \
    && apt-get -y install fonts-symbola fonts-ocr-b \
    && apt-get -y install xorg openbox libnss3 libasound2 \
    && apt-get -y install curl \
    && rm -rf /var/lib/apt/lists/* 

COPY bin/Release/netcoreapp3.1/publish start.sh health.sh /pdfapi

WORKDIR /pdfapi
ENTRYPOINT [ "./start.sh" ]
HEALTHCHECK CMD [ "./health.sh" ]