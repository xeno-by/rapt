After being loaded with SampleInput example, Rapture will think a bit and spawn a series of text files on your hard disk. Each of those files represents calcuating the next generation of clauses as specified by HowItWorks, namely:

Generation 1:

```
History
(1) [[~is a type(v: X), ~extends(v: Y, v: X), ~is a type(v: Y), is parent of(v: X, v: Y)]]
(2) [[is a type(c: C1)]]
(3) [[is a type(c: C2)]]
(4) [[extends(c: C1, c: C2)]]
(5) [[~is parent of(c: C2, c: C1)]]

Front of generation 0
(1) [[~is a type(v: X), ~extends(v: Y, v: X), ~is a type(v: Y), is parent of(v: X, v: Y)]]
(2) [[is a type(c: C1)]]
(3) [[is a type(c: C2)]]
(4) [[extends(c: C1, c: C2)]]
(5) [[~is parent of(c: C2, c: C1)]]


(1) + (2) via [1.1] & [2.1] = [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
Added as (6)
(1) + (2) via [1.3] & [2.1] = [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
Added as (7)
(1) + (3) via [1.1] & [3.1] = [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
Added as (8)
(1) + (3) via [1.3] & [3.1] = [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
Added as (9)
(1) + (4) via [1.2] & [4.1] = [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
Added as (10)
(1) + (5) via [1.4] & [5.1] = [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]
Added as (11)
(2) + (1) via [2.1] & [1.1] = [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
Scrapped as duplicate of (6)
(2) + (1) via [2.1] & [1.3] = [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
Scrapped as duplicate of (7)
(3) + (1) via [3.1] & [1.1] = [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
Scrapped as duplicate of (8)
(3) + (1) via [3.1] & [1.3] = [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
Scrapped as duplicate of (9)
(4) + (1) via [4.1] & [1.2] = [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (10)
(5) + (1) via [5.1] & [1.4] = [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]
Scrapped as duplicate of (11)
```

Generation 2:

```
History
(1) [[~is a type(v: X), ~extends(v: Y, v: X), ~is a type(v: Y), is parent of(v: X, v: Y)]]
(2) [[is a type(c: C1)]]
(3) [[is a type(c: C2)]]
(4) [[extends(c: C1, c: C2)]]
(5) [[~is parent of(c: C2, c: C1)]]
(6) [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
(7) [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
(8) [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
(9) [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
(10) [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
(11) [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]

Front of generation 1
(6) [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
(7) [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
(8) [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
(9) [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
(10) [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
(11) [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]


(2) + (6) via [2.1] & [6.2] = [[~extends(c: C1, c: C1), is parent of(c: C1, c: C1)]]
Added as (12)
(2) + (7) via [2.1] & [7.1] = [[~extends(c: C1, c: C1), is parent of(c: C1, c: C1)]]
Scrapped as duplicate of (12)
(2) + (8) via [2.1] & [8.2] = [[~extends(c: C1, c: C2), is parent of(c: C2, c: C1)]]
Added as (13)
(2) + (9) via [2.1] & [9.1] = [[~extends(c: C2, c: C1), is parent of(c: C1, c: C2)]]
Added as (14)
(2) + (10) via [2.1] & [10.2] = [[~is a type(c: C2), is parent of(c: C2, c: C1)]]
Added as (15)
(2) + (11) via [2.1] & [11.3] = [[~is a type(c: C2), ~extends(c: C1, c: C2)]]
Added as (16)
(3) + (6) via [3.1] & [6.2] = [[~extends(c: C2, c: C1), is parent of(c: C1, c: C2)]]
Scrapped as duplicate of (14)
(3) + (7) via [3.1] & [7.1] = [[~extends(c: C1, c: C2), is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (13)
(3) + (8) via [3.1] & [8.2] = [[~extends(c: C2, c: C2), is parent of(c: C2, c: C2)]]
Added as (17)
(3) + (9) via [3.1] & [9.1] = [[~extends(c: C2, c: C2), is parent of(c: C2, c: C2)]]
Scrapped as duplicate of (17)
(3) + (10) via [3.1] & [10.1] = [[~is a type(c: C1), is parent of(c: C2, c: C1)]]
Added as (18)
(3) + (11) via [3.1] & [11.1] = [[~extends(c: C1, c: C2), ~is a type(c: C1)]]
Added as (19)
(4) + (7) via [4.1] & [7.2] = [[~is a type(c: C2), is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (15)
(4) + (8) via [4.1] & [8.1] = [[~is a type(c: C1), is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (18)
(4) + (11) via [4.1] & [11.2] = [[~is a type(c: C2), ~is a type(c: C1)]]
Added as (20)
(5) + (7) via [5.1] & [7.3] = [[~is a type(c: C2), ~extends(c: C1, c: C2)]]
Scrapped as duplicate of (16)
(5) + (8) via [5.1] & [8.3] = [[~extends(c: C1, c: C2), ~is a type(c: C1)]]
Scrapped as duplicate of (19)
(5) + (10) via [5.1] & [10.3] = [[~is a type(c: C2), ~is a type(c: C1)]]
Scrapped as duplicate of (20)
```

Generation 3:

```
History
(1) [[~is a type(v: X), ~extends(v: Y, v: X), ~is a type(v: Y), is parent of(v: X, v: Y)]]
(2) [[is a type(c: C1)]]
(3) [[is a type(c: C2)]]
(4) [[extends(c: C1, c: C2)]]
(5) [[~is parent of(c: C2, c: C1)]]
(6) [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
(7) [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
(8) [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
(9) [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
(10) [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
(11) [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]
(12) [[~extends(c: C1, c: C1), is parent of(c: C1, c: C1)]]
(13) [[~extends(c: C1, c: C2), is parent of(c: C2, c: C1)]]
(14) [[~extends(c: C2, c: C1), is parent of(c: C1, c: C2)]]
(15) [[~is a type(c: C2), is parent of(c: C2, c: C1)]]
(16) [[~is a type(c: C2), ~extends(c: C1, c: C2)]]
(17) [[~extends(c: C2, c: C2), is parent of(c: C2, c: C2)]]
(18) [[~is a type(c: C1), is parent of(c: C2, c: C1)]]
(19) [[~extends(c: C1, c: C2), ~is a type(c: C1)]]
(20) [[~is a type(c: C2), ~is a type(c: C1)]]

Front of generation 2
(12) [[~extends(c: C1, c: C1), is parent of(c: C1, c: C1)]]
(13) [[~extends(c: C1, c: C2), is parent of(c: C2, c: C1)]]
(14) [[~extends(c: C2, c: C1), is parent of(c: C1, c: C2)]]
(15) [[~is a type(c: C2), is parent of(c: C2, c: C1)]]
(16) [[~is a type(c: C2), ~extends(c: C1, c: C2)]]
(17) [[~extends(c: C2, c: C2), is parent of(c: C2, c: C2)]]
(18) [[~is a type(c: C1), is parent of(c: C2, c: C1)]]
(19) [[~extends(c: C1, c: C2), ~is a type(c: C1)]]
(20) [[~is a type(c: C2), ~is a type(c: C1)]]


(2) + (18) via [2.1] & [18.1] = [[is parent of(c: C2, c: C1)]]
Added as (21)
(2) + (19) via [2.1] & [19.2] = [[~extends(c: C1, c: C2)]]
Added as (22)
(2) + (20) via [2.1] & [20.2] = [[~is a type(c: C2)]]
Added as (23)
(3) + (15) via [3.1] & [15.1] = [[is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (21)
(3) + (16) via [3.1] & [16.1] = [[~extends(c: C1, c: C2)]]
Scrapped as duplicate of (22)
(3) + (20) via [3.1] & [20.1] = [[~is a type(c: C1)]]
Added as (24)
(4) + (13) via [4.1] & [13.1] = [[is parent of(c: C2, c: C1)]]
Scrapped as duplicate of (21)
(4) + (16) via [4.1] & [16.2] = [[~is a type(c: C2)]]
Scrapped as duplicate of (23)
(4) + (19) via [4.1] & [19.1] = [[~is a type(c: C1)]]
Scrapped as duplicate of (24)
(5) + (13) via [5.1] & [13.2] = [[~extends(c: C1, c: C2)]]
Scrapped as duplicate of (22)
(5) + (15) via [5.1] & [15.2] = [[~is a type(c: C2)]]
Scrapped as duplicate of (23)
(5) + (18) via [5.1] & [18.2] = [[~is a type(c: C1)]]
Scrapped as duplicate of (24)
```

Generation 4:

```
History
(1) [[~is a type(v: X), ~extends(v: Y, v: X), ~is a type(v: Y), is parent of(v: X, v: Y)]]
(2) [[is a type(c: C1)]]
(3) [[is a type(c: C2)]]
(4) [[extends(c: C1, c: C2)]]
(5) [[~is parent of(c: C2, c: C1)]]
(6) [[~extends(v: Y, c: C1), ~is a type(v: Y), is parent of(c: C1, v: Y)]]
(7) [[~is a type(v: X), ~extends(c: C1, v: X), is parent of(v: X, c: C1)]]
(8) [[~extends(v: Y, c: C2), ~is a type(v: Y), is parent of(c: C2, v: Y)]]
(9) [[~is a type(v: X), ~extends(c: C2, v: X), is parent of(v: X, c: C2)]]
(10) [[~is a type(c: C2), ~is a type(c: C1), is parent of(c: C2, c: C1)]]
(11) [[~is a type(c: C2), ~extends(c: C1, c: C2), ~is a type(c: C1)]]
(12) [[~extends(c: C1, c: C1), is parent of(c: C1, c: C1)]]
(13) [[~extends(c: C1, c: C2), is parent of(c: C2, c: C1)]]
(14) [[~extends(c: C2, c: C1), is parent of(c: C1, c: C2)]]
(15) [[~is a type(c: C2), is parent of(c: C2, c: C1)]]
(16) [[~is a type(c: C2), ~extends(c: C1, c: C2)]]
(17) [[~extends(c: C2, c: C2), is parent of(c: C2, c: C2)]]
(18) [[~is a type(c: C1), is parent of(c: C2, c: C1)]]
(19) [[~extends(c: C1, c: C2), ~is a type(c: C1)]]
(20) [[~is a type(c: C2), ~is a type(c: C1)]]
(21) [[is parent of(c: C2, c: C1)]]
(22) [[~extends(c: C1, c: C2)]]
(23) [[~is a type(c: C2)]]
(24) [[~is a type(c: C1)]]

Front of generation 3
(21) [[is parent of(c: C2, c: C1)]]
(22) [[~extends(c: C1, c: C2)]]
(23) [[~is a type(c: C2)]]
(24) [[~is a type(c: C1)]]


(2) + (24) via [2.1] & [24.1] = [[]]
Added as (25)

YAY! PROOF FOUND
```