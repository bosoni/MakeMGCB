mgcb /incremental Content.mgcb

echo copying...
mkdir ../Tests/bin/Debug/Content
mkdir ../Tests/bin/Release/Content
cp -rf bin/DesktopGL/Data/* ../Tests/bin/Debug/Content
cp -rf bin/DesktopGL/Data/* ../Tests/bin/Release/Content
cp -rf Data/*.txt ../Tests/bin/Debug/Content
cp -rf Data/*.txt ../Tests/bin/Release/Content
