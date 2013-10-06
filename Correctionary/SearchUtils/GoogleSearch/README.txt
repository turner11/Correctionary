/******************************************************************************
 * Google Search .NET - API for Google Search Services in C# .NET
 *
 * Version: 0.1.5.0 (July 18, 2010)
 * 
 * Copyright (c) 2010, Itay Adler. All rights reserved. 
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided
 * that the following conditions are met: 
 * 
 *   * Redistributions of source code must retain the above copyright notice, this list of conditions
 *      and the following disclaimer. 
 *   * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 *      and the following disclaimer in the documentation and/or other materials provided with the
 *      distribution. 
 *   * Neither the name of the Author nor the names of contributors may be used to endorse or promote
 *      products derived from this software without specific prior written permission. 
 *     
 * THIS SOFTWARE IS PROVIDED BY ITAY ADLER ''AS IS'' AND ANY  EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ITAY ADLER BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGE. 

Google Search .NET for .NET Framework 3.5

-- Overview
Google Search .NET provides .NET developers access to Google search results through a friendly C# API.

The API is based on the Google AJAX Search REST API, which currently supports the following Google services:
Web, Local, Video, Blog, News, Book, Image, Patent.

The API library is built on .NET Framework 3.5.
Google AJAX API - http://code.google.com/apis/ajaxsearch/documentation/reference.html#_intro_fonje

-- Special thanks
David Elentok - For all the help with the design.

-- Usage example
WebQuery query = new WebQuery("Insert query here");
query.StartIndex.Value = 2;
query.HostLangauge.Value = Languages.English;
IGoogleResultSet<GoogleWebResult> resultSet = GoogleService.Instance.Search<GoogleWebResult>(query);