#!/bin/bash
echo "AGENT_BUILDDIRECTORY"
ls -1 $AGENT_BUILDDIRECTORY
echo "SYSTEM_HOSTTYPE is $SYSTEM_HOSTTYPE"
echo 'Acessando pasta para executar build'
cd '/home/marraia/myagent/_work/1/s'
echo 'Executar Docker Compose'
/usr/local/bin/docker-compose up -d
