# PreRequisite: must have installed nvs
nvs add node/12.18.2
nvs use 12.18.2
rootDir=$(pwd)
npm update
npm i -g ionic
cd $rootDir/dashboard
ionic serve
