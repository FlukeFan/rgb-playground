// JScript source code

function createSilverlight()

{  
    Sys.Silverlight.createObject("Page1.xaml", pe, "AgControl1",
                                 {width:'800', height:'600', inplaceInstallPrompt:false, background:'white', isWindowless:'false', framerate:'30', version:'0.95'},
                                 {onError:null, onLoad:null},
                                 null);
                                 
                                 
}


     
