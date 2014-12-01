DIR=$( cd "$( dirname '${BASH_SOURCE[0]}' )" && pwd )

mono "$DIR/lib/csAnt/bin/Release/net-40/csAnt.exe" "$@" -b=$DIR
