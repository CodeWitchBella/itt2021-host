# How to build for linux

Since I was developing on Windows and needed MacOS to build mac version I went
with a bit of a weird route. Following is description how I did it but it's not
by any means the easiest way to do this.

From windows box: first open build settings, then select target linux. Then open
project settings and disable OpenXR support (windows-only). Then press build.
Delete debug files, and zip everything else. Send resulting zip to my mac.

On mac: unzip the zip. Run

```
chmod +x host/Host.x86_64
tar cf host-v12.tar host
xz -9vkT0 host-v12.tar
gzip -9 host-v12.tar
```

Then upload resulting file using [cyberduck](https://cyberduck.io/)
