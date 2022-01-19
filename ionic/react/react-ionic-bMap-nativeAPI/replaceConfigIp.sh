#!/bin/bash
if [ $# -ne 1 ]
then
    echo "Error Input. Please ./replaceConfigIp.sh 10.86.23.12"
    echo 2 # error input
fi

ip=$1
pattern="[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}\.[0-9]\{1,3\}"

# Please make sure this script's working dir is in the upper directory of dashboard
for file in $(find ./dashboard/public/assets/json -name config.json)
do
    echo "----------- modify ${file} ..."
    sed -i -e "/basicUrl*/s/${pattern}/${ip}/g" \
           -e "/videoUrl*/s/${pattern}/${ip}/g" \
            $file
done

echo "----------- config file has finished."