from dataclasses import dataclass
from typing import List

@dataclass
class Switch:
    name: str
    id: int
    rpiPinStraight: int
    rpiPinDiverging: int
    state: str

@dataclass
class Servo:
    name: str
    id: int
    rpiPin: int
    pmwClosedToOpen: List[float]
    pmwOpenToClosed: List[float]
    state: str
    
    
@dataclass
class LED:
    name: str
    id: int
    rpiPinOn: int
    rpiPinOff: int
    state: str