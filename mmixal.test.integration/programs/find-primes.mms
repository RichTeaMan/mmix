﻿L IS 500
t IS $255
n GREG 0
q GREG 0
r GREG 0
jj GREG 0
kk GREG 0
pk GREG 0
mm IS kk
LOC Data_Segment
PRIME1 WYDE 2
LOC PRIME1+2*L
ptop GREG @
j0 GREG PRIME1+2-@
BUF OCTA 0

LOC #100
Main SET n,3
SET jj,j0
2H STWU n,ptop,jj
INCL jj,2
3H BZ jj,2F
4H INCL n,2
5H SET kk,j0
6H LDWU pk,ptop,kk
DIV q,n,pk
GET r,rR
BZ r,4B
7H CMP t,q,pk
BNP t,2B
8H INCL kk,2
JMP 6B
GREG @
Title BYTE "First Five Hundred Primes"
NewLn BYTE #a,0
Blanks BYTE "   ",0
2H LDA t,Title
TRAP 0,Fputs,StdOut
NEG mm,2
3H ADD mm,mm,j0
LDA t,Blanks
TRAP 0,Fputs,StdOut
INCL mm,2*L/10
PBN mm,2B
LDA t,NewLn
TRAP 0,Fputs,StdOut
CMP t,mm,2*(L/10-1)
PBNZ t,3B
TRAP 0,Halt,0
