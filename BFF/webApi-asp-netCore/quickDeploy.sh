#!/bin/bash
#Qickly deploy

echo "Deploy program......"

cd deploy || exit
bash ./deploy.sh

echo "Run program......"
dotnet build

cd webApi

dotnet run
