#!/bin/bash

createList (){
   for filename in ${@:2}
   do
      if [[ $filename == *"depends.on"* ]]
      then
         createList $(dirname $filename) $(< $filename)
      else
         echo $1/$filename
      fi
   done
}


listOfDeps=$(< $1)
listOfCompose=$(createList . $listOfDeps | sort -u)

composesStr=""
for compose in ${listOfCompose[@]}
do
    composesStr+=" -f $compose"
done

echo $composesStr

docker-compose $composesStr pull --ignore-pull-failures && docker-compose $composesStr up --remove-orphans
