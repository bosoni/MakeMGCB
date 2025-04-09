MakeMGCB
(c) mjt, 2019-2025
Released under MIT-license.

Program creates .mgcb files.

Command line parameters:
	-compress
	-compresstex  compress textures
	-mipmap  generate mipmaps
	-tangent  generate tangents

Usage:  MakeMGCB.exe -tangent
creates .mgcb files with tangent frames.

Exported files:
Content.mgcb contains all effects, image, sound and fbx files.
Content_fx.mgcb contains only effects.
Content_fbx.mgcb contains only models.

Assets under music/  will use Song processor.

Under scripts/ directory, there are windows and linux scripts which compiles differend assets using monogame pipeline tools.
Edit them as you will.
