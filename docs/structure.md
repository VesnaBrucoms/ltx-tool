# Structure of the LTX File Format

**Header**
char		8	magicNumber			LIQDTEXT
float		4	version				Version of the format. Is 1.0.
int32		4	textEntries			Count of text entries

**Text Entry**
byte		1	unknown
int32		4	unknown
int32		4	numCharacters		Indicates the number of characters (including `00 00` terminator) in the following string
unicode[]	2	dialogue			The text entry