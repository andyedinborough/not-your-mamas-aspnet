#!/bin/bash
#clean out the last publish
rm -rf bin/ obj/

#build
bower install
dotnet restore
dotnet publish -c Release

#stop the service
ssh -i ../andy.pem ubuntu@edinborough.org 'sudo service dotnetcore stop'

#deploy
rsync -uaz --delete --progress -e 'ssh -i ~/repos/andy.pem' bin/Release/netcoreapp1.1/publish/ ubuntu@edinborough.org:/home/ubuntu/dotnetcore

#start web service
ssh -i ../andy.pem ubuntu@edinborough.org 'sudo service dotnetcore start'