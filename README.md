# Amazon Drive Encoder
Amazon Drive is basically Google Drive. You have the option to upload files and store them in the cloud. The differences between the two are minor, however Google Drive offers you 15GB free for all your files (documents, pictures, applications, etc.).

Amazon Drive offers you unlimited pictures (no matter how much space it requires), and 5GB for videos and other files for free. Since it allows for unlimited pictures, I got the idea to trick the platform into thinking all files I upload are pictures, resulting in unlimited disk space for all of my files.

In order to accomplish the task of tricking Amazon Drive into thinking all of my files are pictures, all that needs to be done is adding an image header to the beginning of the file.

Basically, I take the original files' bytes, and append a PNG header to the beginning of it, then write it into a new file, preserving the original file. The end result is the original file impersonating an image, and bypassing Amazon Drive's limited storage for non-picture files.
