
	0	1	2	3	4	5	6	7	8	9	10	11	12	13	14	15	16	17	18	19	20
0																			
1											x									
2										x	x	x								
3									x	x	x	x	x							
4								p	y	y	y	y	y	z						
5							p	p	p	y	y	y	z	z	z					
6						p	p	p	p	p	y	z	z	z	z	z				
7					w	q	q	q	q	q	r	s	s	s	s	s	k			
8				w	w	w	q	q	q	r	r	r	s	s	s	k	k	k		
9			w	w	w	w	w	q	r	r	r	r	r	s	k	k	k	k	k	
10																			

Up Triangles:
Start - x,y
Cont  - (x-1, x, x+1),y+1
        (x-2, x-1, x, x+1, x+2),y+2

Starts - (10,1) (7,4) (13,4) (4,7) (10,7) (16,7)

Down triangles:
Start - x,y
Cont - (x-1, x, x+1),y-1
       (x-2, x-1, x, x+1, x+2),y-2

Starts - (10,6) (7,9) (13,9)

OUTSIDES:
=========
NW - 
Start - 10,1
Cont - x-1,y+1

NE -
Start - 10,1
Cont - x+1,y+1

Bottom -
Start - 2,9
Cont - x+2,y

INSIDES:
========
Top - 
Start - 6,5
Cont - x+1,y

SW - 
Start - 6,5
Cont - x,y+1;x+1,y
eg (5,5) (5,6;6,6) (6,7;7,7) (7,8;8,8) (8,9;9,9)

SE -
Start - 14,5
Cont - x,y+1;x-1,y
eg (13,5) (13,5;12,6) (12,7;11,7) (11,8;10,8) (10,9;9,9)



Hexagonals:
===========
Down triangle:
Start - x,y
Cont - (x-2, x-1, x, x+1, x+2),y-1
            (x-2, x-1, x+1, x+2),y
            (x-1, x, x+1), y+1

Up triangle:
Start - x,y
Cont - (x-1, x, x+1),y-1
            (x-2, x-1, x+1, x+2),y
            (x-2, x-1, x, x+1, x+ 2), y+1