GITURL=https://user:password@website.scm.azurewebsites.net:443/website.git
COMMENT=$(git log -1 --pretty=%B)

rm -rf deploy
git clone $GITURL deploy
rm -rf deploy/**
cp -r build/ deploy

pushd deploy
git add .
git commit -m"$COMMENT"
git push $GITURL
popd