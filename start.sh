#!/bin/sh
exec dotnet PdfApi.dll --urls "http://0.0.0.0:$PORT"