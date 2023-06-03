#!/bin/sh
tag=`date +%Y%m%d_%H%M`

# publish dotnet
dotnet publish -c Release -p:UseAppHost=false
[ "$?" -ne "0" ] && exit 1

# build docker image
docker build --pull --progress=plain -t pdfapi:$tag -t pdfapi:latest -f Dockerfile .
[ "$?" -ne "0" ] && exit 2

# finish
echo "finished $tag"