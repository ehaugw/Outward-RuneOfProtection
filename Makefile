include Makefile.helpers
modname = RuneOfProtection
dependencies = TinyHelper

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname) --no-print-directory
	
	# sideloader specific
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles

	mkdir -p public/$(sideloaderpath)/Items/RuneOfProtection/Textures
	cp -u resources/icons/rune_of_protection.png                       public/$(sideloaderpath)/Items/RuneOfProtection/Textures/icon.png

forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
