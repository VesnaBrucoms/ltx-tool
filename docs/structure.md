# Structure of the LTX File Format

LTX files are broken down into two sections:
* header
* text entries

## Header
The header stores general file information.

| Type  | Bytes | Name        | Description                          |
| ----- | ----- | ----------- | ------------------------------------ |
| char  | 8	    | magicNumber | Format identifier. Always `LIQDTEXT` |
| float	| 4	    | version     | Always 1.0                           |
| int32	| 4	    | textEntries | Number of text entries in the file   |

## Text Entries
For each text entry:

| Type                          | Bytes | Name           | Description                                                                                                    |
| ----------------------------- | ----- | -------------- | -------------------------------------------------------------------------------------------------------------- |
| byte                          | 1	    | unknown        | Line number?                                                                                                   |
| int32	                        | 4	    | unknown        | Page number?                                                                                                   |
| int32	                        | 4	    | characterCount | Number of characters in stored string (including `00 00` terminator). Multiply by 2 to get the number of bytes |
| string (UTF-16 little endian)	| x	    | text           | The stored string. Includes some custom formatting, which is still unknown in how it is interpreted            |