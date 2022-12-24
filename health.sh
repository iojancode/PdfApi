#!/bin/sh
curl --fail -s "http://localhost:$PORT/health" || exit 