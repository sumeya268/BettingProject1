Balance tests.

test 1- Balance updates when placing a bet
Steps: Log in, add a selection, enter a stake, place bet  
Expected: Balance goes down correctly  
Actual:  yes worked
Status: Pass 

test 2- balance stays the same on failed bet
Steps: Log in, add a selection, break API or turn backend off, place bet  
Expected: Balance does not change  
Actual:  did not work at first but edited later and worked
Status: Fail at first then pass

Test 3 – Balance shows correctly on page load
Steps: Log in and open the homepage  
Expected: Balance displays the correct amount  
Actual:  yes succesfully worked
Status: Pass 

test 4– Balance updates in the header
Steps: Log in, place a bet, check the header  
Expected: Header balance updates straight away  
Actual:  yes successfully worked
Status: Pass
