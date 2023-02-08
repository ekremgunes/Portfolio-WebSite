/**
 * Created by luis on 12/23/16.
 */

(function($, window){
    /**
     * This variable contains the state of the YouTube Frame API. Since the function onYouTubeIframeAPIReady is called
     * only once, we set this variable for videos that added later
     */
    window.c47YTIframeReady = false;

    /**
     * An array contains all YouTube players
     */
    window.c47YTPlayers = [];

    /**
     * This is the outer class of the video background. The background contains 3 levels of block elements
     * -- most outer (relative position, z-index -9999)
     *   -- c37-ultimate-bg (absolute position)
     *     -- the video iframe/video tag (html5)/img (image) z-index: 0;
     *     -- the overlay div to cover the video/image to avoid clicking on the video z-index: 1
     */
    var outerDivClass = 'c47-ultimate-bg';

    /**
     * This is the exception class. It contains the error message that report back to user when errors occur
     */
    function C47Exception(message)
    {
        this.name = "UltimateBackgroundException";
        this.message = message;
    }

    /**
     * This function calculate the optimal width and height of the background div base on the ration 16/9.
     * -- When the div that needs the background is short (height less than 9), then the background will have the
     * width of the div and a calculated height based on ration 16/9. Doing so will avoid black border around videos
     * and image stretches
     *
     * -- When the div that needs the background is narrow (width is less than 16), then the background will use
     * the height of the div and the width is calculated based on the ratio. A part of the the background is cut off
     * but the video and the image will not be stretched in a different ratio other than the native 16/9
     */
    function perfectDimensions(element, options)
    {
        var width = element.outerWidth();
        var height = element.outerHeight();

        console.log('viewport height: ' + $(window.top).height());

        var calculatedHeight = Math.floor(width * 1/(options.ratio));
        var calculatedWidth = Math.floor(height * options.ratio);

        if (options.container == 'body')
        {
            if (options.type=='image')
            {
                var cwidth = Math.ceil($(window).height() * options.ratio);
                if (cwidth < width)
                    return [width, calculatedHeight];
                else
                    return [cwidth, $(window).height()];

            }
            if (options.type == 'youtube')
            {
                var cwidth = Math.ceil($(window).height() * options.ratio);
                if (cwidth < width)
                    return [width, calculatedHeight];
                else
                    return [cwidth, $(window).height()];

            }
        }

        console.log('height: ' + height + '------ calculated height' + calculatedHeight);
        console.log('width: ' + width + '------ calculated width' + calculatedWidth);
        var appliedWidth, appliedHeight;
        if (height >= calculatedHeight)
        {
            console.log('using original height and calculated width');
            appliedHeight = height;
            appliedWidth = calculatedWidth;
        } else if (calculatedHeight >=height)
        {
            console.log('using original width and calculated height');
            appliedWidth = width;
            appliedHeight = calculatedHeight;
        }

        return [appliedWidth, appliedHeight];

    }

    /**
     * This function populate youtube video into a div with divID.
     */
    function populateYouTubeVideo(divID, videoSource, width, height)
    {
        console.log('start a new player with video ID: ' + videoSource);
        var nPlayer = new YT.Player(divID, {
            videoId: videoSource,
            width: width,
            height: height,
            playerVars: {
                'autoplay': 1,
                'controls': 0,
                'showinfo': 0,
                'loop': 1,
                'start': 0,
                'rel' : 0,
                'modestbranding': 1
            },
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
        window.c47YTPlayers = window.c47YTPlayers || [];

        window.c47YTPlayers.push(nPlayer);
    }


    $.fn.c47bg = function(options)
    {
        var element = this;

        element.css('position', 'relative');
        var defaults = {
            ratio: 16/9, // usually either 4/3 or 16/9 -- tweak as needed
            mute: true,
            repeat: true,
            crop: true
            // container: "body",//body or div. If body, we will set the positioning of the video/image container to fixed, otherwise, set it to absolute
            // type: 'youtube',//image, self-hosted, youtube
        };
        //element.css('position', 'relative');
        options = $.extend(options, defaults);
        console.log(options);
        /**
         * Remove margin, padding of the body and HTML IF container is body. Otherwise, if
         * the container is div, removing padding and margin for body and html may cause conflicts
         */
        console.log('tag name: ', this.prop('tagName'));
        if (this.prop('tagName').toLowerCase === "body")
        {
            console.log('removing padding and margin in body and html');
            $('body,html').css('margin', 0);
            $('body,html').css('padding', 0);
        }

        /**
         * Get the width and height of the element that we want to apply the video background to
         * (the container)
         */


        var dimensions = perfectDimensions(element, options);
        var width = dimensions[0];
        var height = dimensions[1];

        /**
         * Create a random ID to associate with the video/image container
         * @type {string}
         */
        var randomID = 'c47-random-' + Math.floor(Math.random() * 100000);

        /**
         * make the excess part of the video hidden. Only apply this when the container is a div
         * Otherwise, it will disable scrolling of the body, which is undesirable
         */

        if (options.crop && options.container != "body")
        {
            //console.log('we are cropping');
            //element.css('overflow', 'hidden');
        }

        if ( options.type == "youtube")
        {
            //insert the script tag to access YouTube iFrame API
            var tag = document.createElement('script');
            tag.src = "https://www.youtube.com/iframe_api";
            var firstScriptTag = document.getElementsByTagName('script')[0];
            firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

            element.find('.' + outerDivClass).remove();

            var videoPosition = options.container=='body' ?'fixed;' : 'absolute';

            videoContainer =
                '<div style="position: absolute; top: 0; left: 0; overflow: hidden; z-index: -1;" class="'+outerDivClass+'">' +
                '<div style="-webkit-backface-visibility: hidden; backface-visibility: hidden;position:'+videoPosition+';" id="'+randomID+'" ></div>' +
                '</div>';
            element.prepend(videoContainer);

            var checkIframeReady = setInterval(function(){
                if (window.c47YTIframeReady)
                {
                    clearInterval(checkIframeReady);
                    populateYouTubeVideo(randomID, options.source, width, height);
                }

            }, 10);

            window.onYouTubeIframeAPIReady = function() {
                window.c47YTIframeReady = true;
            };

            window.onPlayerReady = function(event)
            {
                event.target.playVideo();
                event.target.mute();
            };

            window.onPlayerStateChange = function(state)
            {
                if (state.data === 0 && options.repeat) {

                    console.log('ok to repeat', window.c47YTPlayers.length);
                    for (var i =0 ; i < window.c47YTPlayers.length; i++)
                    {
                        console.log(window.c47YTPlayers[i].getPlayerState());
                        if (window.c47YTPlayers[i].getPlayerState() == 0)
                            window.c47YTPlayers[i].seekTo(0);
                    }
                }
            }


        } else if (options.type == "self-hosted")
        {
            /**
             * In case the user wants to use html5 video as background, the source property needs
             * to be an object. {mp4: url, webm: url}
             */
            if (typeof options.source != "object")
                throw new C47Exception('The source property needs to be an object. Please go to https://github.com/datmt/ultimate-background for documentation');

            var source = options.source;


            var poster = '', mp4Source = '', ogvSource = '', webmSource = '';

            if (typeof options.poster != "undefined")
                poster = options.poster;

            if (typeof source.mp4 != "undefined")
                mp4Source = '<source src="'+source.mp4+'" type="video/mp4">';

            if (typeof source.ogv != "undefined")
                ogvSource = '<source src="'+source.ogv+'" type="video/ogg">';

            if (typeof source.webm != "undefined")
                webmSource = '<source src="'+source.webm+'" type="video/webm">';


            if (mp4Source == ogvSource == webmSource == '')
                throw new C47Exception('Please provide at least one video source');

            var videoContainer =
                '<div class="'+outerDivClass+'" style="z-index: -1; position: absolute; top: 0; left: 0; overflow: hidden;">'+
                '<video  autoplay="1" loop="1" muted="1" id="'+randomID+'" height="'+height+'" width="'+width+'" poster="'+poster+'" >'+
                mp4Source + ogvSource + webmSource +
                'Your browser doesn\'t support HTML5 video tag.'+
                '</video>'+
                '</div>';

            element.prepend(videoContainer);
        } else if (options.type == "image")
        {
            var position = options.container == "body" ? "fixed" : "absolute";
            var imageContainer =
                '<div style="position: absolute; top: 0; left: 0; z-index: -1;" class="'+outerDivClass+'">' +
                '<img style="position: '+position+'; top: 0; left: 0;" id="'+randomID+'" src="'+options.source+'" />' +
                '</div>';

            element.prepend(imageContainer);

        }
        //apply width and height to the video, image element


        $(function(){

            //apply z-index to the background div
            var backgroundDiv = $('.' + outerDivClass);

            //create an absolute overlay div to cover the video (avoid clicking) and also apply transparent image if needed
            var overlayClass = typeof options.overlayClass == "undefined" ? "b1" :  options.overlayClass;

            //create an absolute overlay div to cover the video (avoid clicking) and also apply transparent image if needed
            var overlayPosition = options.container == "body" ? "fixed" : "absolute";
            if (backgroundDiv.find('.c47-overlay').length ==0)
                backgroundDiv.append('<div class="c47-overlay '+overlayClass+'" style="position: '+overlayPosition+'; padding: 0; margin: 0; top: 0; bottom: 0; right: 0; left: 0; width: '+width+'px; height: '+height+'px; z-index: 100;"></div>');


            try
            {
                $('.c47-overlay').addEventListener('touchmove', function(e) {

                    e.preventDefault();

                }, false);

                backgroundDiv.addEventListener('touchmove', function(e) {

                    e.preventDefault();

                }, false);
            } catch (e)
            {

            }

            //on resize, change the width and height of the video to match the size of its parent
            $(window).bind('resize orientationchange', function(){

                console.log('resizing bg...;');
                var dimensions = perfectDimensions($(this), options);

                var appliedWidth = dimensions[0];
                var appliedHeight = dimensions[1];

                var bg;
                var outerDiv = $(element).find('.'+outerDivClass).first();
                outerDiv.css('height', $(element).outerHeight());
                outerDiv.css('width', '100%');

                if (options.type == 'youtube')
                {
                    bg = outerDiv.find('iframe').first();
                    bg.attr('width', appliedWidth);
                    bg.attr('height', appliedHeight);
                    bg.css('top', 0);
                    bg.css('left', 0);

                } else if (options.type == 'image')
                {
                    bg = outerDiv.find('img').first();
                    bg.css('width', appliedWidth + 'px');
                    bg.css('height', appliedHeight + 'px');

                    var marginLeft = appliedWidth/2 - $(window).width()/2;

                    if (marginLeft > 0 && $(window).height()/$(window).width() >1.4)
                    {
                        bg.css('margin-left', '-'+marginLeft+'px');
                    } else
                    {
                        bg.css('margin-left', '0');
                    }

                } else if (options.type == 'self-hosted')
                {
                    bg =outerDiv.find('video');
                    bg.attr('width', appliedWidth);
                    bg.attr('height', appliedHeight);
                }
                bg.siblings('.c47-overlay').css('width', appliedWidth + 'px');
                bg.siblings('.c47-overlay').css('height', appliedHeight + 'px');

            });

            $(function(){
                console.log('triggering rezie manually');
                $(element).trigger('resize');
            });
        });
        return this;
    };


}(jQuery, window));