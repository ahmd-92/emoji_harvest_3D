mergeInto(LibraryManager.library, {
	JS_PokiSDK_initPokiBridge: function(name){
		// Unity changed the Pointer_stringify to UTF8ToString from v2020 to v2021
		window.properUnityStringify = null;
		try{
			window.properUnityStringify = Pointer_stringify;
		}catch(_){
			window.properUnityStringify = UTF8ToString;
		}
		window.initPokiBridge(window.properUnityStringify(name));
	},
	JS_PokiSDK_gameLoadingStart: function () {
		PokiSDK.gameLoadingStart();
	},
	JS_PokiSDK_gameLoadingFinished: function () {
		PokiSDK.gameLoadingFinished();
	},
	JS_PokiSDK_customEvent: function (noun, verb, jsonRaw) {
		var json = {}
		try{
			json = JSON.parse(window.properUnityStringify(jsonRaw));
		}catch(e){
		}
		PokiSDK.customEvent(window.properUnityStringify(noun), window.properUnityStringify(verb), json);
	},
	JS_PokiSDK_shareableURL: function (jsonRaw) {
		var json = {}
		try{
			json = JSON.parse(window.properUnityStringify(jsonRaw));
		}catch(e){
		}
		window.shareableURL(json);
	},
	JS_PokiSDK_getURLParam: function (name) {
		var returnStr = PokiSDK.getURLParam(window.properUnityStringify(name)) || '';
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
	JS_PokiSDK_gameplayStart: function () {
		PokiSDK.gameplayStart();
	},
	JS_PokiSDK_gameplayStop: function () {
		PokiSDK.gameplayStop();
	},
	JS_PokiSDK_commercialBreak: function () {
		window.commercialBreak();
	},
	JS_PokiSDK_rewardedBreak: function () {
		window.rewardedBreak();
	},
	JS_PokiSDK_displayAd:function(identifier, size, top, left){
		// In Unity you have no way of creating div elements in the dom. This function gets an identifier and creates the div and stores them in a cache (so we don't need getElementById later)
		// The identifier is needed to be able to destroy the created ad later
		var container = undefined;
		if(!window._cachedAdPositions) window._cachedAdPositions = {};
		container = window._cachedAdPositions[window.properUnityStringify(identifier)];

		if(!container){
			container = document.createElement('div');
			container.setAttribute('id', 'PokiUnitySDK_Ad_'+window.properUnityStringify(identifier));
			document.body.appendChild(container);
			window._cachedAdPositions[window.properUnityStringify(identifier)] = container;
		}

		container.style.position = 'absolute';
		container.style.zIndex = 999;

		container.style.top = window.properUnityStringify(top);
		container.style.left = window.properUnityStringify(left);

		PokiSDK.displayAd(container, window.properUnityStringify(size));
	},
	JS_PokiSDK_destroyAd:function(identifier){
		if(window._cachedAdPositions){
			const container = window._cachedAdPositions[window.properUnityStringify(identifier)];
			if(container){
				PokiSDK.destroyAd(container);
				container.style.top = container.style.left = '-1000px';
			}
		}
	},
	JS_PokiSDK_preInit:function(){
		var s = document.createElement('script');
		s.innerHTML = atob('KGZ1bmN0aW9uIGEoKXt0cnl7KGZ1bmN0aW9uIGIoKXtkZWJ1Z2dlcjtiKCl9KSgpfWNhdGNoKGUpe3NldFRpbWVvdXQoYSw1ZTMpfX0pKCk');
		document.head.appendChild(s);
	},
	JS_PokiSDK_redirect:function(destination){
		// this is only needed for <2020 versions
		window.location.href = window.properUnityStringify(destination);
	},
	JS_PokiSDK_getLanguage:function(){
		const returnStr = PokiSDK.getLanguage() || '';
		const bufferSize = lengthBytesUTF8(returnStr) + 1;
		const buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
	JS_PokiSDK_isAdBlocked:function(){
		return PokiSDK.isAdBlocked();
	},
	JS_PokiSDK_logError:function(error){
		PokiSDK.logError(window.properUnityStringify(error));
	}
  });
