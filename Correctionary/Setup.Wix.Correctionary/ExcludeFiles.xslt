<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>

  <!--Identity Transform-->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
 


  <!--Set up keys for ignoring various file types-->
  <!--PDB-->
  <xsl:key name="pdb-search" match="wix:Component[contains(wix:File/@Source, '.pdb')]" use="@Id"/>
  <!--CS-->
  <xsl:key name="cs-search" match="wix:Component[contains(wix:File/@Source, '.cs')]" use="@Id"/>
  
  <!--SVN-->
  <xsl:key name="svn-search" match="wix:Component[ancestor::wix:Directory/@Source = '.svn']" use="@Id"/>
 
  <!--Known files to ommit-->
  <xsl:key name="Preferences-search" match="wix:Component[contains(wix:File/@Source, 'Preferences')]" use="@Id"/>
  <xsl:key name="RecipeParams-search" match="wix:Component[contains(wix:File/@Source,  'RecipeParams')]" use="@Id"/>
  <xsl:key name="FTD2XX_NET-search" match="wix:Component[contains(wix:File/@Source, 'FTD2XX_NET.xml')]" use="@Id"/>
  <xsl:key name="errlog-search" match="wix:Component[contains(wix:File/@Source, 'errlog.txt')]" use="@Id"/>

  <!--NOTE: In every couple the first line get rid of the componentl the second line gets rid of the ComponentRef-->
    <!---Match and ignore .pdb files-->
  <xsl:template match="wix:Component[key('pdb-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('pdb-search', @Id)]"/>

  <!--Match and ignore leftover .cs files -->
  <xsl:template match="wix:Component[key('cs-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('cs-search', @Id)]"/>

  <!---Match and ignore ".svn" Directory -->
  <xsl:template match="wix:Directory[@Name = '.svn']"/>
  <xsl:template match="wix:ComponentRef[key('svn-search', @Id)]"/>


  
  <!--/*____________________________________________________________________________________________________________*/-->
  <!---Match and ignore "known files"  -->
   <xsl:template match="wix:Component[key('Preferences-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('Preferences-search', @Id)]"/>

  <xsl:template match="wix:Component[key('RecipeParams-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('RecipeParams-search', @Id)]"/>

  <xsl:template match="wix:Component[key('FTD2XX_NET-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('FTD2XX_NET-search', @Id)]"/>

  <xsl:template match="wix:Component[key('errlog-search', @Id)]"/>
  <xsl:template match="wix:ComponentRef[key('errlog-search', @Id)]"/>


</xsl:stylesheet>