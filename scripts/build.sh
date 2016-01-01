#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"


mkdir -p $DIR/../obj
mkdir -p $DIR/../bin

pushd $DIR/../obj

cmake -DCMAKE_INSTALL_PREFIX=$DIR/../bin ../src
make

popd