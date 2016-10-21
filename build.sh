mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe restore
mono .paket/paket.exe generate-include-scripts type fsx framework net452

yarn install

rm -rf build
mkdir -p build/bin

fsharpc src/WebHost.fsx --out:build/bin/Website.dll --target:library --warn:5 --nologo
webpack --config webpack.deploy.config.js -p

cp -r src/img/ build/img
cp -r src/templates/ build/templates
cp src/web.config build
cp packages/build/FSharp.Core/lib/net40/FSharp.Core.dll build/bin

maincss=$(basename `ls build/content/main*css`)
bundlejs=$(basename `ls build/content/bundle*js`)
sed -i "" "s/main.css/$maincss/" build/templates/master.html
sed -i "" "s/bundle.js/$bundlejs/" build/templates/master.html

while read line || [[ -n "$line" ]]; do
  if [[ $line == *"dll"* ]]
  then
    file=`expr "$line" : '.*\(packages[^"]*\)'`
    cp $file build/bin
  fi
done <paket-files/include-scripts/net452/include.main.group.fsx