#!/bin/bash
echo 'Acessando pasta para executar build'
cd '/home/marraia/myagent/_work/1/s'
echo 'Executar Docker Compose'
ssh -t marraia sudo docker-compose up -d
