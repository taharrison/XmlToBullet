# XmlToBullet
Visualize the structure of Xml documents easily, with human readable output (great for stakeholders!)


Usage:

    XmlToBullet.exe <inputpath> <outputpath>

Options:

    -a=<attribute bullet point symbol>      specify the bullet point used for attributes (default '+')

    -noAttributes                           do not show attributes at all

    -help                                   show helptext";


Example:

With document.xml as

    <doc xmlns="foo.bar.com/schemas/baz">
        <customers>
            <customer id="1" rank="Captain">
                <FirstName>Patrick</FirstName>
                <LastName>Stewart</LastName>
            </customer>
            <customer id="2">
                <FirstName>Tony</FirstName>
                <LastName>Todd</LastName>
                <Notes>Candyman?</Notes>
            </customer>
        </customers>
    </doc>

Running

    XmlToBullet.exe document.xml documentout.txt

Creates a textfile with

    * doc
      + xmlns e.g. "foo.bar.com/schemas/baz"
      * customers
        * customer (...many...)
          + id e.g. "1"
          + rank e.g. "Captain"
          * FirstName e.g. "Patrick"
          * LastName e.g. "Stewart"
          * Notes e.g. "Candyman?"

##Features

* Human readable
* All xml elements and attributes in the input document are discovered and documented - even if they are optional
* Example data is shown for all elements that contain data at least once in the document
* Can ignore attributes
* Repetative elements are de-duplicated and marked as lists with a "(...many...)" suffix
* Really helps with understanding ridiculously long xml nightmare documents.

##License

This tool was open sourced with permission from Burrows, whose digital department believes in giving back to the community.