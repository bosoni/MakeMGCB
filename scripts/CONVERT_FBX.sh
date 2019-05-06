mgcb /incremental Content_fbx.mgcb

rm -rf animations
rm -rf effects
rm -rf fonts
rm -rf images
rm -rf models
rm -rf music
rm -rf sounds
echo copying...
mkdir ../Tests/bin/Debug/Content
mkdir ../Tests/bin/Release/Content
cp -rf bin/DesktopGL/Data/* ../Tests/bin/Debug/Content
cp -rf bin/DesktopGL/Data/* ../Tests/bin/Release/Content
