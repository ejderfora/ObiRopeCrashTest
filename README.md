# ObiRopeCrashTest

The problem is when we set active obi rope int the scene and we reload scene after that we occured a crush the next seting active obi rope.
Check The CrashTestGameobject in hierarchy

Press 'L' for setActive OBI ROPE in xrplayer/autohand/beltCylinder in hierarchy
Press 'K' for resetting active scene
It is crashing after setActive(L)->reset scene(K)->second setActive(L)
