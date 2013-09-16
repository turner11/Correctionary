NTextCat 0.2.0
http://ntextcat.codeplex.com

NTextCat is text classification utility (tool and API).
Primary target is language identification. So it helps you to recognize (identify) the language of text (or binary) snippet.
NTextCat is inspired by famous Perl utility for language identification: TextCat

Languages available out of the box:
-----------------------------------
* Wikipedia-MostCommon-Legacy__All-Utf8: 280+ languages (and flavors) extracted from *.Wikipedia.org and encoded in UTF-8. Additionally 83 most popular lanugages encoded in their respective "legacy" encodings (e.g. 1252, Big5, etc.). THE DEFAULT.
* Wikipedia-All-Utf8: 280+ languages (and flavors) extracted from *.Wikipedia.org and encoded in UTF-8 only.
* Wikipedia-MostCommon-LegacyAndUtf8: 83 most popular languages extracted from *.Wikipedia.org and encoded in UTF-8 and their respective "legacy" encodings (e.g. 1252, Big5, etc.)
* Wikipedia-MostCommon-Utf8: 83 most popular languages extracted from *.Wikipedia.org and encoded in UTF-8 only.
* TextCat: 74 languages from original TextCat tool.

Recommended input: snippet of text with more than 50 words.

Features:
---------
* .Net Framework 4.0 support
* DISCONTINUED (Should be compatible with Mono 2.10 but hasn't been tested in this release): .Net Framework 3.5 Client Profile support (compatible with Mono 2.6.7). Mono 2.6.7 is also shipped with Ubuntu 10.10 and 11.04
* Pure .Net application (C#).


Example of usage (default settings used):
-----------------------------------------
	NTextCatLegacy.exe -noprompt < Evaluation\ukrainian-1251.txt

First result returned is considered the best. Format is "<lanugage>_cp<codepage>". E.g. uk_cp1251


How to identify language using command line interface
-----------------------------------------------------
With Use of NTextCat as console application which is capable of training (creating language models) and classifying new snippet of text into one or more classes of known languages.
http://ntextcat.codeplex.com/wikipage?title=How%20to%20identify%20language%20from%20command%20line&referringTitle=Home

How to identify language using managed API
------------------------------------------
With Use of NTextCat as library that you can reference from your application to empower it with language identification capabilities.
http://ntextcat.codeplex.com/wikipage?title=NTextCat.Lib.Legacy%20samples&referringTitle=Home