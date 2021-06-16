# RegroupSubtitles
**RegroupSubtitles** rewrites SubRip (`.srt`) files where new lines have been put in groups together with already displayed lines, resulting in lines being shifted "upwards".

**Before:**

```
1
00:00:00,560 --> 00:00:05,220

foo bar

2
00:00:05,220 --> 00:00:05,230
foo bar
 

3
00:00:05,230 --> 00:00:08,310
foo bar
baz foo bar

4
00:00:08,310 --> 00:00:08,320
baz foo bar
 

5
00:00:08,320 --> 00:00:12,150
baz foo bar
baz

6
00:00:12,150 --> 00:00:12,160


 
```

**After:**

```
1
00:00:00,560 --> 00:00:05,220
foo bar


2
00:00:05,220 --> 00:00:05,230
 


3
00:00:05,230 --> 00:00:08,310
baz foo bar


4
00:00:08,310 --> 00:00:08,320
 


5
00:00:08,320 --> 00:00:12,150
baz


6
00:00:12,150 --> 00:00:12,160
 


```

## Usage

```
regroupsubtitles.exe path searchPattern

```

**Example:**
```
regroupsubtitles.exe "C:\Temp" "*.srt"

```