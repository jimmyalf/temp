
var numberOfSteps = 5;

$(function () {
    $("#progressbar").progressbar({
        value: getPercentage(1)
    });

    $(".step2 #progressbar").progressbar({
        value: getPercentage(2)
    });

    $(".step3 #progressbar").progressbar({
        value: getPercentage(3)
    });

    $(".step4 #progressbar").progressbar({
        value: getPercentage(4)
    });

    $(".step5 #progressbar").progressbar({
        value: getPercentage(5)
    });

});

function getPercentage(currentStep) {
    if (currentStep == numberOfSteps) 
    {
        return 100; 
    }
    return (100/numberOfSteps)*currentStep;
}