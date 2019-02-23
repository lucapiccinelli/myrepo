#!/bin/bash

createList (){
   local directoryName=$1
   local useDevForCurrentProject=$2
   local listOfDependencies=${@:3}
   
   for fileAndDirname in $listOfDependencies; do
      local dependencyDirname=$(dirname $fileAndDirname)
      local filename=$(basename $fileAndDirname)
      local currentDirectory=$(pwd)

      cd $dependencyDirname

      if [[ $filename == *"depends.on"* ]]; then
         createList $dependencyDirname $useDevForCurrentProject $(< $filename)
      else
         echo $(pwd)/$filename
      fi
      cd $currentDirectory 
   done
}


dependenciesFilename=$1
useCurrentProject=${2:-true} 

listOfDeps=$(< $dependenciesFilename)
listOfCompose=$(createList . $useCurrentProject $listOfDeps | sort -u)

composesStr=""
for compose in ${listOfCompose[@]}; do
    composesStr+=" -f $compose"
done

echo $composesStr

#docker-compose $composesStr pull --ignore-pull-failures && docker-compose $composesStr up --remove-orphans
