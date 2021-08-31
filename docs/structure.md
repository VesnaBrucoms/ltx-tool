# Structure of the LTX File Format

Possibly based off LaTeX word processing format?

**Header**
* char		8	magicNumber			LIQDTEXT
* float		4	version				Version of the format. Is 1.0.
* int32		4	textEntries			Count of text entries

**Text Entry**
For each text entry:
* byte		1	unknown				Line number? Entry in grouping ID?
* int32		4	unknown				Page number? Grouping ID?
* int32		4	characterCount		Indicates the number of characters (including `00 00` terminator) in the following string. Multiply by 2 to get the number of bytes to read.
* unicode[]	2	text				The text entry.