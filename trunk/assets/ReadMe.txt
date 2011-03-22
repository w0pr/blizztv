
                 .: Copyright (C) 2010-2011, BlizzTV Project :.
 ___________________________________________________________________________

                        BlizzTV - e-Sports aggregator 

 ___________________________________________________________________________

 v0.22.4098.35659-beta                                http://get.blizztv.com

 BlizzTV is an open source all-in-one e-Sports aggregator that can track feeds, 
 podcasts, streams, videos, blue-posts and events.

 Requirements: 
 ------------- 
 * .Net Framework 4.0
 * Adobe Flash Player 10
 * Visual C++ 2010 Redistributable x86

 Support:
 --------
 * FAQ    : http://www.blizztv.com/topic/95-frequently-asked-questions/
 * Forums : http://www.blizztv.com/forum/24-blizztv-application/

 What's New:
 -----------

 * Faster modules & architecture code.
 * Improved 'add new subscription' functionality.

 Changelog:
 ----------

 * Module architecture got an overhaul and re-factored it. It should be faster & more 
   stable now.
 * Improved modules data process & refresh routines - they're 2x to 4x faster now.
 * Created an installer for BlizzTV. The setup will now check all required dependencies 
   and will download & install them as needed. Tested the installer for most possible.  
 * configurations; WinXP, WinXP-64, Vista, Vista-64, Win7, Win7-64.
 * Re-designed the 'Add New Subscription' window from scratch. For streams and videos 
   module it'll be asking for the content-slug instead of the full url.
 * Improved modules drag & drop subscription handling. User will now be notified with a 
   hour-glass cursor when application tries to handle drag & dropped urls.
 * When a subscription already exists for the drag & dropped url - it will now be correctly 
   handled.
 * Settings & storage will now be stored in C:\users\%user%\AppData\Local\BlizzTV\ folder.
 * Added more text-beautifier filters for Events module TeamLiquid parser. 
 * Fixed a few glitches in stream and video module's provider handling & media processing 
   code.
 * Stream chat windows will no longer snap to it's parent player when it's in border-less 
   mode.
 * Fixed a few glitches on preferences form.
 * Added a volume control to notifications tab in preferences window.
 * Fixed a small bug in update installer.
 * Fixed a few-glitches on configuration-wizard.

 Notes:
 ------
 * This version is released with an installer. To preserve your existing subscriptions, copy
 subscriptions.db from your old installation folder to C:\users\%user%\AppData\Local\BlizzTV\.

