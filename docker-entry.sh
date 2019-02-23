#!/bin/bash

createList (){
   local directoryName=$1
   local depToExclude=$2
   local listOfDependencies=${@:3}
   
   for fileAndDirname in $listOfDependencies; do
      local dependencyDirname=$(dirname $fileAndDirname)
      local filename=$(basename $fileAndDirname)
      local currentDirectory=$(pwd)

      cd $dependencyDirname
      if [[ $filename == *"depends.on"* ]]; then
         createList $dependencyDirname $depToExclude $(< $filename)
      else
         if [ $(realpath $fileAndDirname) != $depToExclude ]; then
            echo $(pwd)/$filename
         fi
      fi
      cd $currentDirectory
   done
}

dependenciesFilename=$1
useCurrentProject=${2:-true}
depToExclude=.
if [ $useCurrentProject != true ]; then
   depToExclude=$(pwd)/docker-compose.yml
fi


listOfDeps=$(< $dependenciesFilename)

cd $(dirname $dependenciesFilename)
listOfCompose=$(createList . $depToExclude $listOfDeps | sort -u)

composesStr=""
for compose in ${listOfCompose[@]}; do
    composesStr+=" -f $compose"
done

echo $composesStr

docker-compose $composesStr pull --ignore-pull-failures && docker-compose $composesStr up --remove-orphans
