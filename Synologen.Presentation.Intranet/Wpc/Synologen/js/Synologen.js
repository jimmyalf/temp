$(document).ready(function () {
    //$(".fancybox").fancybox();

    var numberOfSteps = 6;

    $(function () {

        for (var i = 1; i <= numberOfSteps; i++) 
        {
            $(".step" + i + " #progressbar").progressbar({
                value: getPercentage(i)
            });
        }

    });

    function getPercentage(currentStep) {
        if (currentStep == numberOfSteps) {
            return 100;
        }
        return (100 / numberOfSteps) * currentStep;
    }
});

